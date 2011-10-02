using System;
using System.Collections.Generic;
using BatchJobs.Core;
using BatchJobs.Core.Logging;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace BatchJobs.UnitTests
{
    [TestFixture]
    public class JobAgentBaseTester
    {
        [Test]
        public void Should_loop_through_the_batches()
        {
        	BatchLoggerFactory.Default = () => new StubLogger();
            StateTransitions.Called = 0;
            var agent = new JobAgentStub(new StubAgentFactory(1));
            agent.Execute();
            Assert.That(StateTransitions.Called,Is.EqualTo(3));
        }
        
        [Test]
        public void Should_loop_through_each_transitions_for_each_batch()
        {
			BatchLoggerFactory.Default = () => new StubLogger();
            StateTransitions.Called = 0;
            var agent = new JobAgentStub(new StubAgentFactory(3));
            agent.Execute();
            Assert.That(StateTransitions.Called, Is.EqualTo(9));            
        }
    }

    public class StubAgentFactory : IStateTransitionFactory
    {
        public StubAgentFactory():this(1)
        {
        }

        public StubAgentFactory(int numberOfTransitions)
        {
            _numberOfTransitions = numberOfTransitions;
        }

        private int _numberOfTransitions;

        public IEnumerable<IStateTransition<T>> GetAll<T>()
        {
             for (int i = 0; i < _numberOfTransitions; i++)
                yield return (IStateTransition<T>)
                                                new StateTransitions();
        }
    }

    public class StateTransitions:IStateTransition<Batch>    
    {
        public static int Called { get; set; }
        
        public bool IsValid(Batch batch)
        {
            Called++;
            return false;
        }

        public void Execute(Batch batch)
        {
            throw new NotImplementedException();
        }
    }

    public class JobAgentStub:JobAgentBase<Batch>
    {
        public JobAgentStub(IStateTransitionFactory factory) : base(factory)
        {
            
        }

        protected override Batch[] GetNextEntites()
        {
            return new[]
                       {
                           new Batch(),
                           new Batch(),
                           new Batch(), 
                       };
        }
    }

    public class Batch
    {
    }
}