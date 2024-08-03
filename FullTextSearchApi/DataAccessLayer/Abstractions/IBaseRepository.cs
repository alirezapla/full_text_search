namespace FullTextSearchApi.DataAccessLayer.Abstractions;

public interface IBaseRepository<T> where T:class
{
    Task GetAsync();
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
    
}