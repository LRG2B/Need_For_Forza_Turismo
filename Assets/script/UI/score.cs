using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour
{

    public Text dist_parc_score;



    public void update_score(int new_dist)
    {
        dist_parc_score.text = new_dist.ToString();
    }

}
