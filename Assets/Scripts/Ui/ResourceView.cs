using TMPro;
using UnityEngine;

namespace Game.Ui
{
    public class ResourceView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI text;
        
        public void SetAmount(int amount)
        {
            text.text = amount.ToString();
        }
    }
}