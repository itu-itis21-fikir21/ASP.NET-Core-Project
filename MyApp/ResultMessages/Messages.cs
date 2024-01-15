using System.Drawing;

namespace MyApp.ResultMessages
{
	public static class Messages
	{
		public static class Article
		{
			public static string Add(string articleTitle)
			{
				return $"Article titled {articleTitle} was added successfully."; 
			}
			public static string Update(string articleTitle)
			{
				return $"Article titled {articleTitle} was updated successfully.";

			}
			public static string Delete(string articleTitle)
			{
				return $"Article titled {articleTitle} was deleted successfully.";

			}
            public static string UndoDelete(string articleTitle)
            {
                return $"Article titled {articleTitle} was restored successfully.";
            }
        }

		public static class Category
		{
			public static string Add(string categoryName)
			{
				return $"Category titled {categoryName} was added successfully.";
			}
			public static string Update(string categoryName)
			{
				return $"Category titled {categoryName} was updated successfully.";

			}
			public static string Delete(string categoryName)
			{
				return $"Category titled {categoryName} was deleted successfully.";

			}
            public static string UndoDelete(string categoryName)
            {
                return $"Category titled {categoryName} was restored successfully.";
            }
        }
		public static class User
		{
			public static string Add(string userName)
			{
				return $"User named {userName} was added successfully.";
			}
			public static string Update(string userName)
			{
				return $"User named {userName} was updated successfully.";
			}
			public static string Delete(string userName)
			{
				return $"User named {userName} was deleted successfully.";
			}
		}
	}
}
