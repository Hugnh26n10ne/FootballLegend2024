using UnityEngine;
using UnityEngine.UI;

public class SliderFollow : MonoBehaviour
{
    public Slider slider;
    public Transform target; // Thay thế bằng transform của nhân vật hoặc đối tượng muốn theo dõi

    void Update()
    {
        if (target != null)
        {
            slider.transform.position = target.transform.position;
        }
    }
}
