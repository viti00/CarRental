namespace CarRental.Servces.DealerService
{
    public interface IDealerService
    {
        public Task<bool> Approve(int id);

        public bool Reject(int id);
    }
}
