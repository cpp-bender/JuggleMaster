using UnityEngine;

namespace HyperBase
{
    public class WaitForMouseDown : CustomYieldInstruction
    {
        public override bool keepWaiting
        {
            get
            {
                return !Input.GetMouseButtonDown(0);
            }
        }
    }
}
