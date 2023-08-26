using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStage : MonoBehaviour
{
    public string nextStage = "Stage_01";
    public bool replay = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            if (nextStage == "Stage_01" || nextStage.Trim() == "" || nextStage == null)
            {
                nextStage = SceneManager.GetActiveScene().name;

                char a = nextStage.ToCharArray()[nextStage.Length - 1];
                if (a != '9')
                {
                    nextStage = nextStage.Substring(0, nextStage.Length - 1) + (int.Parse(a.ToString()) + 1).ToString();
                }
                else
                {
                    char b = nextStage.ToCharArray()[nextStage.Length - 2];
                    nextStage = nextStage.Substring(0, nextStage.Length - 2) + (int.Parse(b.ToString()) + 1).ToString() + "0";
                }
                //Debug.Log("next : " + nextStage);
            }

            Message message = new Message();
            message.nextStageName = nextStage;
            message.replay = replay;
            MessageManager.Instance.SendMessage(MessageManager.MessageId.NextStage, message);
        }
    }
}
