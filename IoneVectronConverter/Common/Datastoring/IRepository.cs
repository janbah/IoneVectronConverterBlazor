namespace IoneVectronConverter.Common.Datastoring;

public interface IRepository<T>
{
    IQueryable<T> Load();
    void Insert(T entity);
    void Update(T entity);
    void Delete(int id);
}