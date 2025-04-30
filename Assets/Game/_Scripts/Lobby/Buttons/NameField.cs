using TMPro;
using UnityEngine;

namespace RTS
{
    public class NameField : MonoBehaviour
    {
        private TMP_InputField inputField;

        private void Start()
        {
            inputField = GetComponent<TMP_InputField>();
            inputField.onEndEdit.AddListener(ChangeName);
        }

        private void ChangeName(string input)
        {
            PlayerPrefs.SetString("Name", input);
            PlayerPrefs.Save();
        }
    }
}
