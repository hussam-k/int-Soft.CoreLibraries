using System.Collections.Generic;
using System.Linq.Expressions;

namespace IntSoft.DAL.Utility
{
    internal class SubstituteExpressionVisitor : ExpressionVisitor
    {
        public Dictionary<Expression, Expression> Subst = new Dictionary<Expression, Expression>();

        protected override Expression VisitParameter(ParameterExpression node)
        {
            Expression newValue;
            return Subst.TryGetValue(node, out newValue) ? newValue : node;
        }
    }
}
