namespace QueryKit;

using System.Linq.Expressions;
using Ardalis.SmartEnum;

public abstract class LogicalOperator : SmartEnum<LogicalOperator>
{
    public static readonly LogicalOperator AndOperator = new AndType();
    public static readonly LogicalOperator OrOperator = new OrType();

    
    public static LogicalOperator GetByOperatorString(string op)
    {
        var logicalOperator = List.FirstOrDefault(x => x.Operator() == op);
        if (logicalOperator == null)
        {
            throw new Exception($"Operator {op} is not supported");
        }
        return logicalOperator;
    }
    
    public abstract string Operator();
    public abstract Expression GetExpression<T>(Expression left, Expression right);
    protected LogicalOperator(string name, int value) : base(name, value)
    {
    }
    
    private class AndType : LogicalOperator
    {
        public AndType() : base("&&", 0)
        {
        }
        
        public override string Operator() => "&&";
        public override Expression GetExpression<T>(Expression left, Expression right)
            => Expression.AndAlso(left, right);
    }

    private class OrType : LogicalOperator
    {
        public OrType() : base("||", 1)
        {
        }
        
        public override string Operator() => "||";
        public override Expression GetExpression<T>(Expression left, Expression right)
            => Expression.OrElse(left, right);
    }
}