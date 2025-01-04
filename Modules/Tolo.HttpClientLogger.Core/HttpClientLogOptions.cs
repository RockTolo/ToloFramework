using System.Linq.Expressions;

namespace Tolo.HttpClientLogger
{
    public class HttpClientLogOptions
    {
        public int RequestBodyLogLimit { get; set; } = 4096;

        public int ResponseBodyLogLimit { get; set; } = 4096;

        public bool IsExceptionToStackTrace { get; set; }

        public HashSet<string> HideLogInfoPropertys { get; set; } = new();

        public void ShouldRedactProperty(Expression<Func<HttpClientLogInfo, object>> expression)
        {
            var visitor = new MemberExpressionVisitor(expression);
            var propertyNames = visitor.GetMembers().Select(t => t.Member.Name);
            ShouldRedactProperty(propertyNames);
        }

        public void ShouldRedactProperty(IEnumerable<string> propertyNames) 
        {
            foreach (var propertyName in propertyNames)
            {
                HideLogInfoPropertys.Add(propertyName);
            }
        }
    }
}
