namespace Pool;

public class Spaceship
{
    private string name = "new_Spaceship";
    private string status = "Alive";
    public void Kill()
    {
        this.status="Killed";
    }

    public void Call()
    {
        System.Console.WriteLine($"{this.name}: {this.status}");
    }

    public void Continue_with_new_spaceship()
    {
        if(this.status!="Alive")
            this.status="Alive";
    }
}
public class Pool<T> where T : new()
{
    Queue<T> pool = new Queue<T>();
    public Pool(int count)
    {
        for (int i = 0; i < count; i++)
        {
            pool.Enqueue(new T());
        }
    }

    public T Give_new_pool_object()
    {
        return pool.Dequeue();
    }

    public void Get_back_to_pool(T pool_object)
    {
        if(pool.Count == count)
            throw new Exception("Cant add new spaceship, pool is full.");
        else
        {
            pool.Enqueue(pool_object);
        }
    }
}

public class PoolManager<T> : IDisposable where T : new()
{
    Pool<T> pool;
    T pool_object;
    public PoolManager(Pool<T> pool)
    {
        pool_object = pool.Give_new_pool_object();
        this.pool = pool;
    }

    public void Dispose()
    {
        pool.Get_back_to_pool(pool_object);
    }

    public T Get_pool_object()
    {
        return pool_object;
    }
}