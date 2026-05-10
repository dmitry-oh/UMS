using UnityEngine;

public class DebugDspTime : MonoBehaviour
{
    private void OnGUI()
    {
        // 텍스트 스타일 설정 (폰트 크기, 색상 등)
        GUI.skin.label.fontSize = 30;
        GUI.contentColor = Color.yellow;

        // 화면 좌측 상단에 dspTime 출력
        double currentDsp = AudioSettings.dspTime;
        GUI.Label(new Rect(20, 20, 500, 100), $"DSP Time: {currentDsp:F4}");
    }
}