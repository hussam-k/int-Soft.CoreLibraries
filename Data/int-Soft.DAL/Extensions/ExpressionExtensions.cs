﻿using System;
using System.Linq.Expressions;
using IntSoft.DAL.Utility;

namespace IntSoft.DAL.Extensions
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> a, Expression<Func<T, bool>> b)
        {
            ParameterExpression p = a.Parameters[0];

            var visitor = new SubstituteExpressionVisitor();
            visitor.Subst[b.Parameters[0]] = p;
            BinaryExpression body = Expression.AndAlso(a.Body, visitor.Visit(b.Body));

            return Expression.Lambda<Func<T, bool>>(body, p);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> a, Expression<Func<T, bool>> b)
        {
            ParameterExpression p = a.Parameters[0];

            var visitor = new SubstituteExpressionVisitor();
            visitor.Subst[b.Parameters[0]] = p;

            Expression body = Expression.OrElse(a.Body, visitor.Visit(b.Body));
            return Expression.Lambda<Func<T, bool>>(body, p);
        }
    }
}