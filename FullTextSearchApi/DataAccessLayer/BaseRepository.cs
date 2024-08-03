using FullTextSearchApi.DataAccessLayer.Abstractions;

namespace FullTextSearchApi.DataAccessLayer;

public abstract class BaseRepository<T>: IBaseRepository<T> where T : class
{
    public  Task GetAsync()
    {
        throw new NotImplementedException();
    }

    public void Create(T entity)
    {
        throw new NotImplementedException();
    }

    public void Update(T entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(T entity)
    {
        throw new NotImplementedException();
    }
}