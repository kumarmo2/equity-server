namespace EquityServer.DTOs;


public class Result<T, E>
{
    private T _success;
    private E _err;

    public Result(T t)
    {
        _success = t;
        _err = default(E);
    }

    public Result(E e)
    {
        _err = e;
        _success = default(T);
    }

    public T Ok { get { return _success; } }
    public E Err { get { return _err; } }
}
