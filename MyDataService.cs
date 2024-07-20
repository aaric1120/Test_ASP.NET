public interface IMyDataService{
    Task ProcessDataAsync(MyData data);
}

public class MyDataService: IMyDataService {
    private readonly IMyDataRepository _repository;

    public MyDataService(IMyDataRepository repository) {
        _repository = repository;
    }

    public async Task ProcessDataAsync(MyData data) {
        Console.WriteLine(data.Name);
        Console.WriteLine(data.ID);

        await _repository.SaveAsync(data);
    }
}