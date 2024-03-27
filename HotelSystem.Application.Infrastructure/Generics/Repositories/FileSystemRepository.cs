using System.Text.Json;
using System.Text.Json.Nodes;
using HotelSystem.Application.Domain.Generic.Generics.Repositories;
using HotelSystem.Application.Entities;
using Microsoft.Extensions.Configuration;

namespace HotelSystem.Application.Infrastructure.Generics.Repositories;

public class FileSystemRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    private readonly string BASEPATH;
    private string ModelPath;
    public FileSystemRepository(IConfiguration configuration)
    {
        BASEPATH = (configuration.GetSection("DATA_TYPE").Value == "FS" ? configuration.GetSection("FS_FOLDER").Value : string.Empty) ?? string.Empty ;
        CreateFileSystemEstructure();
    }

    private void CreateFileSystemEstructure()
    {
        ModelPath = $"{BASEPATH}{typeof(TEntity).Name}";
        if (!Directory.Exists(ModelPath)) 
            Directory.CreateDirectory(ModelPath);
        if(!File.Exists($"{ModelPath}/_metadata.json"))
            File.WriteAllText($"{ModelPath}/_metadata.json",JsonSerializer.Serialize(new
            {
                TOTAL_REGISTRIES = 0,
                LAST_INDEX = Guid.Empty
            }));
    }

    public async Task<TEntity> FindById<Key>(Key id) where Key : struct
    {
        var file = Directory.GetFiles(ModelPath).ToList().Select(c =>
        {
            return new FileInfo(c);
        }).FirstOrDefault(c => c.Name == $"{id}.json");
        var fileContent = await File.ReadAllTextAsync(file.FullName);
        var model = JsonSerializer.Deserialize<TEntity>(fileContent);
        return model;
    }

    public async Task<TEntity> Add(TEntity entity)
    {
        await File.WriteAllTextAsync($"{ModelPath}/{entity.Id}.json",JsonSerializer.Serialize(entity));
        var fileContent = await File.ReadAllTextAsync($"{ModelPath}/_metadata.json");
        var coder = JsonNode.Parse(fileContent);
        coder["TOTAL_REGISTRIES"] = coder["TOTAL_REGISTRIES"].GetValue<int>() + 1;
        coder["LAST_INDEX"] = entity.Id;
        await File.WriteAllTextAsync($"{ModelPath}/_metadata.json",JsonSerializer.Serialize(coder));
        return entity;
    }

    public async Task<IEnumerable<TEntity>> FindAllAsync()
    {
        var files = Directory.GetFiles(ModelPath).ToList().Select(c =>
        {
            return new FileInfo(c);
        }).Where(c => c.Name != "_metadata.json");
        var resultList = new List<TEntity>();
        foreach (var file in files)
        {
            var fileContent = await File.ReadAllTextAsync(file.FullName);
            var model = JsonSerializer.Deserialize<TEntity>(fileContent);
            if (model != null) resultList.Add(model);
        }
        return resultList;
    }

    public async Task Update(TEntity entity)
    {
        await File.WriteAllTextAsync($"{ModelPath}/{entity.Id}.json",JsonSerializer.Serialize(entity));
    }
}