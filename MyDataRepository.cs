public interface IMyDataRepository {
    Task SaveAsync(MyData data);
}

public class MyDataRepository : IMyDataRepository {
    private readonly ApplicationDbContext _context;

    public MyDataRepository(ApplicationDbContext context) {
        _context = context;
    }

    public async Task SaveAsync(MyData data) {
        _context.MyData.Add(data);
        await _context.SaveChangesAsync();
    }
}