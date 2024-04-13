using UnityEngine;

namespace Game.Ui
{
    public class DeathController : MonoBehaviour
    {
        [SerializeField]
        private GameObject deathPanel;
        
        [SerializeField]
        private GameObject sunDeath;
        [SerializeField]
        private GameObject noSuppliesDeath;
        [SerializeField]
        private GameObject notCollectedDeath;
        [SerializeField]
        private GameObject zombiDeath;

        public void Show(FailType fail)
        {
            sunDeath.SetActive(fail == FailType.Sun);
            noSuppliesDeath.SetActive(fail == FailType.NoSupplies);
            notCollectedDeath.SetActive(fail == FailType.NotCollected);
            zombiDeath.SetActive(fail == FailType.EatenByZombi);
            deathPanel.SetActive(true);
        }

        public void Hide()
        {
            deathPanel.SetActive(false);
        }
    }
}