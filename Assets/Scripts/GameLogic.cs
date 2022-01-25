using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    private GameObject outerCircle;
    public GameObject encodeText;
    public GameObject encodeOutput;
    public Button encode;
    public Button decode;
    public GameObject decodeText;
    public GameObject decodeOutput;

    // Start is called before the first frame update
    void Start()
    {
        outerCircle = this.gameObject;
        encode.onClick.AddListener(Encode);
        decode.onClick.AddListener(Decode);
    }

    // Update is called once per frame
    void Update()
    {
        //Rotate outer circle
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            outerCircle.gameObject.transform.Rotate(0, 0, -13.85f, Space.Self);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            outerCircle.gameObject.transform.Rotate(0, 0, 13.85f, Space.Self);
        }
    }

    // Create new coded/decoded string with shifted alphabet
    public string ButtonPressed(int shiftValue, char[] original)
    {
        char[] alpha = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
        string encoded = "";

        for (int i = 0; i < original.Length; i++)
        {
            if (!char.IsLetter(original[i]))
            {
                encoded += original[i];
            }
            else
            {
                bool upper = char.IsUpper(original[i]);
                int position = (char.ToUpper(original[i]) - 65) + shiftValue;
                // Adjust position to wrap around alphabet
                if (position < 0)
                {
                    position = 26 + position;
                }
                else if (position > 25)
                {
                    position = position - 26;
                }

                if (upper)
                {
                    encoded += char.ToUpper(alpha[position]);
                }
                else
                {
                    encoded += alpha[position];

                }
            }
        }
        return encoded;
    }

    // Get input to be encoded and change output text
    public void Encode()
    {
        encode.gameObject.SetActive(false);

        float rotation = outerCircle.gameObject.transform.eulerAngles.z;
        int shiftValue = 0 - Mathf.RoundToInt(rotation / 13.85f);
        char[] original = encodeText.GetComponent<InputField>().text.ToCharArray();

        encodeOutput.GetComponent<InputField>().text = ButtonPressed(shiftValue, original);
        encode.gameObject.SetActive(true);
    }

    // Get output to be decoded and change output text
    public void Decode()
    {
        decode.gameObject.SetActive(false);

        float rotation = outerCircle.gameObject.transform.eulerAngles.z;
        int shiftValue = Mathf.RoundToInt(rotation / 13.85f);
        char[] original = decodeText.GetComponent<InputField>().text.ToCharArray();

        decodeOutput.GetComponent<InputField>().text = ButtonPressed(shiftValue, original);
        decode.gameObject.SetActive(true);

    }
}
