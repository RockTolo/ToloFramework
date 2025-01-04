using System.Linq.Expressions;

namespace Tolo.HttpClientLogger
{
    public class MemberExpressionVisitor : ExpressionVisitor
    {
        private readonly List<MemberExpression> _memberExpressions = new List<MemberExpression>();

        public MemberExpressionVisitor(Expression expression)
        {
            Visit(expression);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            _memberExpressions.Add(node);
            return base.VisitMember(node);
        }

        public List<MemberExpression> GetMembers()
        {
            return _memberExpressions;
        }
    }
}
