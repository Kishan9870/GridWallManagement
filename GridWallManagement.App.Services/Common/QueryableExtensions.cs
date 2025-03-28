using AutoMapper;
using GridWallManagement.App.Models.Common;
using System.ComponentModel;
using System.Linq.Expressions;

namespace GridWallManagement.App.Services.Common
{
    public static class QueryableExtensions
    {
        public static IQueryable<TModel> GetPage<TResponse, TModel>(this IQueryable<TModel> queryable, object request, IMapper mapper)
        {
            if (request is Page page)
            {
                // Search Query
                queryable = GetSearchQuery<TResponse, TModel>(queryable, request, mapper);

                // Order by query
                if (!string.IsNullOrWhiteSpace(page.SortProperty))
                {
                    try
                    {
                        // Create Sort Exp
                        var paramExp = Expression.Parameter(typeof(TResponse), "x");

                        var propertyExp = CreateExpression(paramExp, page.SortProperty);
                        var sortExp = Expression.Lambda(propertyExp, new ParameterExpression[] { paramExp });

                        queryable = page.IsDescending
                            ? queryable.OrderByDescending(mapper.Map<Expression<Func<TModel, object>>>(sortExp))
                            : queryable.OrderBy(mapper.Map<Expression<Func<TModel, object>>>(sortExp));
                    }
                    catch
                    {
                        // Ignore if failed
                    }
                }

                // Pagination
                if (page.PageSize > 0 && page.PageNumber >= 0)
                    queryable = queryable.Skip(page.PageNumber * page.PageSize).Take(page.PageSize);
            }
            return queryable;
        }

        public static List<T> GetListPage<T>(this List<T> queryable, object request)
        {
            if (request is Page page)
            {
                // Pagination
                if (page.PageSize > 0 && page.PageNumber >= 0)
                    queryable = queryable.Skip(page.PageNumber * page.PageSize).Take(page.PageSize).ToList();
            }
            return queryable;
        }

        public static IQueryable<TModel> GetSearchQuery<TResponse, TModel>(this IQueryable<TModel> queryable, object request, IMapper mapper)
        {
            if (request is SearchQuery searchQuery && !string.IsNullOrWhiteSpace(searchQuery.SearchProperty))
            {
                try
                {
                    var responseType = typeof(TResponse);

                    // Create Search Exp
                    var paramExp = Expression.Parameter(responseType, "x");
                    var propertyType = GetPropertyType(responseType, searchQuery.SearchProperty);
                    var propertyExp = CreateExpression(paramExp, searchQuery.SearchProperty);
                    var converter = TypeDescriptor.GetConverter(propertyType);
                    var compareValueExp = Expression.Constant(converter.ConvertTo(searchQuery.SearchValue, propertyType), propertyType);
                    if (propertyType.IsAssignableFrom(typeof(string)) && searchQuery.SearchOption == SearchingOptions.Contains)
                    {
                        // Create Search Exp                           
                        var method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                        var containsMethodExp = Expression.Call(propertyExp, method, compareValueExp);
                        var searchExp = Expression.Lambda<Func<TResponse, bool>>(containsMethodExp, paramExp);
                        queryable = queryable.Where(mapper.Map<Expression<Func<TModel, bool>>>(searchExp));
                    }
                    else
                    {
                        var containsMethodExp = searchQuery.SearchOption switch
                        {
                            (SearchingOptions.GreaterThan) => Expression.GreaterThan(propertyExp, compareValueExp),
                            (SearchingOptions.GreaterThanOrEqual) => Expression.GreaterThanOrEqual(propertyExp, compareValueExp),
                            (SearchingOptions.LessThan) => Expression.LessThan(propertyExp, compareValueExp),
                            (SearchingOptions.LessThanOrEqual) => Expression.LessThanOrEqual(propertyExp, compareValueExp),
                            _ => Expression.Equal(propertyExp, compareValueExp)
                        };

                        var searchExp = Expression.Lambda<Func<TResponse, bool>>(containsMethodExp, paramExp);
                        queryable = queryable.Where(mapper.Map<Expression<Func<TModel, bool>>>(searchExp));
                    }
                }
                catch
                {
                    // Ignore if failed
                }
            }
            return queryable;
        }

        private static Type GetPropertyType(Type refType, string propertyName)
        {
            Type type = refType;
            foreach (var member in propertyName.Split('.'))
                type = type.GetProperty(member).PropertyType;
            return type;
        }

        public static Expression CreateExpression(ParameterExpression param, string propertyName)
        {
            Expression body = param;
            foreach (var member in propertyName.Split('.'))
                body = Expression.Property(body, member);

            return body;
        }

        public static IQueryable<TModel> GetPageWithMultiSearchQuery<TResponse, TModel>(this IQueryable<TModel> queryable, object request, IMapper mapper)
        {
            if (request is MultiSearchQueryPage page)
            {
                if (page.SearchQueries != null && page.SearchQueries.Count > 0)
                {
                    // Apply all the search queries.
                    foreach (var sq in page.SearchQueries)
                    {
                        queryable = GetSearchQuery<TResponse, TModel>(queryable, sq, mapper);
                    }
                }
            }
            return queryable;
        }
    }
}
