using Core.Config;
using Core.Services;
using System;
using System.Collections.Generic;

namespace Core.Sequencer
{
    public class SequenceService : BaseService
    {
        private SequenceDataContainer sequenceDataContainer;

        private Dictionary<string, SequenceData> sequenceDataMap;
        private Sequence currentSequence = null;
        private SequenceFactory sequenceFactory;
        private CommandFactory commandFactory;

        public override void Initialize()
        {
            sequenceDataContainer = ConfigRegistry.GetConfig<SequenceDataContainer>();
            commandFactory = new CommandFactory();
            sequenceFactory = new SequenceFactory(commandFactory);
            sequenceDataMap = sequenceDataContainer.Data;
        }

        public void StartSequence(string sequenceID, int startCommandIndex = 0, Action onComplete = null)
        {
            if (currentSequence == null)
            {
                var sequenceData = sequenceDataMap[sequenceID];
                currentSequence = sequenceFactory.CreateSequence(sequenceData);
                currentSequence.StartSequence(startCommandIndex, onComplete);
            }
        }

        public void Update()
        {
            if (currentSequence != null)
            {
                if (currentSequence.IsDone)
                {
                    currentSequence.FinishSequence();
                    currentSequence = null;
                }
                else
                {
                    currentSequence.Update();
                }
            }
        }

        public Command GetCommandAtIndex(int commandIndex)
        {
            return currentSequence.GetCommandAtIndex(commandIndex);
        }

        public void StopCurrentSequence()
        {
            currentSequence = null;
        }
    }
}
