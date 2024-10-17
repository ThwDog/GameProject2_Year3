public interface Ipauseable{
    // for pause and resume obj
    void pause();
    void resume();
}

// TODO : use in obj that can restart like puzzle
public interface IRestartable
{
    void _Restart();
}

public interface IEnable
{
    void _enable();
}

// this interface for element in map that need to open or some when close
public interface MapOpenable 
{
    void Open();
    void Close();
}

public interface checkBool{
    bool _check();
}


