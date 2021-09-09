using System;
using System.Linq.Expressions;
using GameStore.Domain.Enums;
using GameStore.Domain.Models;

namespace GameStore.Application.Filters.Games
{
    public class PublishedDateFilter : IFilter<Game>
    {
        private readonly DateIssue _dateIssue;

        public PublishedDateFilter(DateIssue dateIssue)
        {
            _dateIssue = dateIssue;
        }

        public Expression<Func<Game, bool>> Execute(Expression<Func<Game, bool>> expression)
        {
            if (_dateIssue != DateIssue.AllTime)
            {
                DateTime dateTime = GetCalculateDate(_dateIssue);
                Expression<Func<Game, bool>> filter = x => x.PublishedDate > dateTime;
                return ToolsExpression.MergeExpression(expression, filter);
            }

            return expression;
        }

        private DateTime GetCalculateDate(DateIssue dateIssue)
        {
            switch (dateIssue)
            {
                case DateIssue.LastWeek:
                    return DateTime.UtcNow.AddDays(-7);
                case DateIssue.LastMonth:
                    return DateTime.UtcNow.AddMonths(-1);
                case DateIssue.LastYear:
                    return DateTime.UtcNow.AddYears(-1);
                case DateIssue.TwoYears:
                    return DateTime.UtcNow.AddYears(-2);
                case DateIssue.ThreeYears:
                    return DateTime.UtcNow.AddYears(-3);
                case DateIssue.AllTime:
                    return default;
                default:
                    return default;
            }
        }
    }
}
