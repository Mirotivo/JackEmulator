public class Array<T>
{
    private T[] items;

    public T this[int index]
    {
        get
        {
            return items[index];
        }
        set
        {
            items[index] = value;
        }
    }

    public Array(int size)
    {
        items = new T[size];
        Memory.alloc(size);
    }

    public void dispose()
    {
        Memory.deAlloc();
    }
}
