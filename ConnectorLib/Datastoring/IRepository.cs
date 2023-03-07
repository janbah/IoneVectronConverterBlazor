namespace ConnectorLib.Datastoring;

public interface IRepository<T>
{
    IQueryable<T> Load();
    long Insert(T entity);
    void Update(T entity);
    void Delete(int id);
    void Clear();
}