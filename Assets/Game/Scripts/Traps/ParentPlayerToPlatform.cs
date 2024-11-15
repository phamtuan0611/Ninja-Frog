using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentPlayerToPlatform : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(transform);

            //Co the hieu la lam dong bo chuyen dong cua Player voi be dung, giup Player khong bi roi hay khong theo kip chuyen dong cua be dung
            //Kieu no dong bo Frame voi nhau thay vi moi dua mot toc do frame
            other.GetComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.None;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);

            other.GetComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.Interpolate;
        }
    }
}
