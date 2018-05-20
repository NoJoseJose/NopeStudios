using UnityEngine;
using UnityEngine.UI;

public class StatusIndicator : MonoBehaviour
{
    [SerializeField] private RectTransform _healthBarRect;
    [SerializeField] private Text _healthText;

	void Start () 
	{
	    if (_healthBarRect == null)
	    {
            //
	    }

	    if (_healthText == null)
	    {
            //
	    }
	}

    public void SetHealth(int current, int max)
    {
        float pct = (float) current / max;
        _healthBarRect.localScale = new Vector3(pct, _healthBarRect.localScale.y, _healthBarRect.localScale.z);
        _healthText.text = current.ToString();
    }
}
