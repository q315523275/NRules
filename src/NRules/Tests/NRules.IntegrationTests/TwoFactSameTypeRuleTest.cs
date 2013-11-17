﻿using NRules.IntegrationTests.TestAssets;
using NRules.IntegrationTests.TestRules;
using NUnit.Framework;

namespace NRules.IntegrationTests
{
    [TestFixture]
    public class TwoFactSameTypeRuleTest : BaseRuleTestFixture
    {
        [Test]
        public void Fire_MatchingFacts_FiresOnce()
        {
            //Arrange
            var fact1 = new FactType3() {TestProperty = "Valid Value"};
            var fact2 = new FactType3() {TestProperty = "Valid Value", Parent = fact1};
            var fact3 = new FactType3() {TestProperty = "Invalid Value", Parent = fact1};
            var fact4 = new FactType3() {TestProperty = "Valid Value", Parent = null};

            Session.Insert(fact1);
            Session.Insert(fact2);
            Session.Insert(fact3);
            Session.Insert(fact4);

            //Act
            Session.Fire();

            //Assert
            AssertFiredOnce();
        }
        
        [Test]
        public void Fire_FirstMatchingFactSecondInvalid_DoesNotFire()
        {
            //Arrange
            var fact1 = new FactType3() {TestProperty = "Valid Value"};
            var fact2 = new FactType3() {TestProperty = "Valid Value"};

            Session.Insert(fact1);
            Session.Insert(fact2);

            //Act
            Session.Fire();

            //Assert
            AssertDidNotFire();
        }

        protected override void SetUpRules()
        {
            SetUpRule<TwoFactSameTypeRule>();
        }
    }
}