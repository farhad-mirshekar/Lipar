namespace Lipar.Data.Seeds
{
   public interface IBaseSeed
    {
		string ModelName { get; }
		string Schema { get; }

		void Add(int index);
		void Update(int index);
		void AddIfNotExist(int index);
		void InsertIfNotExist();

		bool IsAddIfNotExist { get; set; }
		int Count { get; }
	}
}
