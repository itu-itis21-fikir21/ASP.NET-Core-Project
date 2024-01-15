using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
	public interface IDashbordService
	{
		Task<List<int>> GetYearlyArticleCounts();
		Task<int> GetTotalArticleCount();
		Task<int> GetTotalCategoryCount();
		Task<int> GetTotalUserCount();

    }
}
