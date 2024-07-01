namespace ProductosAPI.Services
{
    public interface ICommonService<T, TI, TU> 
    {
        public List<string> Errors { get; }

        Task<IEnumerable<T>> Get();

        Task<T> GetById(int id);

        Task<T> Add(TI insertDto);

        Task<T> Update(TU updateDtoint, int id = -1);

        Task<T> Delete(int id);

        bool ValidateInsert(TI dto);
        bool ValidateUpdate(TU dto);
    }
}
