namespace RuleBender.RuleParsers.RuleMatchers
{
    using System;

    using RuleBender.Entity;

    public class EveryDayHandler : IMailRuleMatcher
    {
        public bool IsProperMatcher(MailRule rule)
        {
            throw new NotImplementedException();
        }

        public bool ShouldBeRun(MailRule rule, DateTime startTime)
        {
            throw new NotImplementedException();
        }
    }
}