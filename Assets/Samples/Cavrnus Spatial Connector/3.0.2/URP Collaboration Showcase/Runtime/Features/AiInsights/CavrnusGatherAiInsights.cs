using Cavrnus.SpatialConnector.API;
using UnityEngine;
using UnityEngine.Events;

namespace Cavrnus.SpatialConnector.Samples.CollaborationShowcase 
{
    public class CavrnusGatherAiInsights : MonoBehaviour
    {
        public UnityEvent OnStarted;
        public UnityEvent OnFinished;
        
        private CavrnusSpaceConnection spaceConn;
        
        private void Start()
        {
            gameObject.SetActive(false);

            CavrnusFunctionLibrary.AwaitAnySpaceConnection(sc =>
            {
                sc.BindSpacePolicy("api:rooms:getRoom", allowed =>
                {
                    gameObject.SetActive(allowed);
                });

                spaceConn = sc;
            });
        }
        
        public void CopyAiInsightsToClipboard()
        {
            if (spaceConn == null)
            {
                print("No Space Connected!");
                return;
            }
            
            DoCopyJournal();
        }
        
        private async void DoCopyJournal()
        {
            OnStarted?.Invoke();
            string history = await CavrnusFunctionLibrary.FetchSpaceHistory(spaceConn);

            GUIUtility.systemCopyBuffer = history;
            Debug.Log("Space History Copied to Clipboard!");
            
            OnFinished?.Invoke();
        }
    }
}