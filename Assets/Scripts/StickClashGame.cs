using UnityEngine;

public class StickClashGame : MonoBehaviour
{
    private float p1 = 0.25f, p2 = 0.75f;
    private float hp1 = 100f, hp2 = 100f;
    private float v1, v2;
    private bool jump1, jump2;
    private float attack1, attack2;
    private string message = "STICK CLASH 2D";

    private void Update()
    {
        float dt = Time.deltaTime;
        if (Input.GetKey(KeyCode.A)) p1 -= 0.35f * dt;
        if (Input.GetKey(KeyCode.D)) p1 += 0.35f * dt;
        if (Input.GetKey(KeyCode.LeftArrow)) p2 -= 0.35f * dt;
        if (Input.GetKey(KeyCode.RightArrow)) p2 += 0.35f * dt;
        if (Input.GetKeyDown(KeyCode.W) && !jump1) { v1 = 0.65f; jump1 = true; }
        if (Input.GetKeyDown(KeyCode.UpArrow) && !jump2) { v2 = 0.65f; jump2 = true; }
        if (Input.GetKeyDown(KeyCode.F)) Attack(true);
        if (Input.GetKeyDown(KeyCode.Return)) Attack(false);
        v1 -= 1.8f * dt; v2 -= 1.8f * dt;
        if (jump1) { p1 = Mathf.Clamp(p1, 0.04f, 0.96f); }
        attack1 = Mathf.Max(0, attack1 - dt); attack2 = Mathf.Max(0, attack2 - dt);
        p1 = Mathf.Clamp(p1, 0.06f, 0.94f); p2 = Mathf.Clamp(p2, 0.06f, 0.94f);
        if (Mathf.Abs(p1 - p2) < 0.055f) { if (attack1 > 0) hp2 = Mathf.Max(0, hp2 - 0.3f); if (attack2 > 0) hp1 = Mathf.Max(0, hp1 - 0.3f); }
        if (hp1 <= 0 || hp2 <= 0) message = hp1 <= 0 ? "PLAYER 2 WINS!" : "PLAYER 1 WINS!";
    }

    private void Attack(bool first)
    {
        if (first) { attack1 = 0.22f; if (Mathf.Abs(p1-p2) < 0.16f) hp2 = Mathf.Max(0, hp2-12); }
        else { attack2 = 0.22f; if (Mathf.Abs(p1-p2) < 0.16f) hp1 = Mathf.Max(0, hp1-12); }
    }

    private void OnGUI()
    {
        float w = Screen.width, h = Screen.height;
        GUIStyle title = new GUIStyle(GUI.skin.label) { fontSize = Mathf.Max(18, (int)(h*0.045f)), alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, normal = { textColor = Color.white } };
        GUIStyle label = new GUIStyle(GUI.skin.label) { fontSize = Mathf.Max(14, (int)(h*0.025f)), normal = { textColor = Color.white } };
        GUI.color = new Color(0.06f,0.08f,0.14f); GUI.DrawTexture(new Rect(0,0,w,h), Texture2D.whiteTexture); GUI.color = Color.white;
        GUI.Label(new Rect(0, h*0.02f, w, h*0.09f), message, title);
        GUI.color = Color.red; GUI.DrawTexture(new Rect(w*0.06f,h*0.13f,w*0.36f,h*0.025f),Texture2D.whiteTexture); GUI.color = Color.green; GUI.DrawTexture(new Rect(w*0.58f,h*0.13f,w*0.36f,h*0.025f),Texture2D.whiteTexture); GUI.color = Color.white;
        GUI.Label(new Rect(w*0.06f,h*0.08f,w*0.3f,h*0.05f),"P1  HP: "+Mathf.CeilToInt(hp1),label); GUI.Label(new Rect(w*0.62f,h*0.08f,w*0.3f,h*0.05f),"P2  HP: "+Mathf.CeilToInt(hp2),label);
        GUI.color = new Color(0.18f,0.22f,0.31f); GUI.DrawTexture(new Rect(0,h*0.78f,w,h*0.22f),Texture2D.whiteTexture); GUI.color = Color.white;
        DrawStick(w*p1,h*0.59f,new Color(0.1f,0.75f,1f), attack1>0); DrawStick(w*p2,h*0.59f,new Color(1f,0.28f,0.3f), attack2>0);
        float by=h*0.83f, bw=w*0.12f, bh=h*0.1f;
        if (GUI.Button(new Rect(w*0.03f,by,bw,bh),"◀")) p1-=0.025f;
        if (GUI.Button(new Rect(w*0.16f,by,bw,bh),"▶")) p1+=0.025f;
        if (GUI.Button(new Rect(w*0.30f,by,bw,bh),"HIT")) Attack(true);
        if (GUI.Button(new Rect(w*0.58f,by,bw,bh),"HIT")) Attack(false);
        if (GUI.Button(new Rect(w*0.72f,by,bw,bh),"◀")) p2-=0.025f;
        if (GUI.Button(new Rect(w*0.85f,by,bw,bh),"▶")) p2+=0.025f;
        if (GUI.Button(new Rect(w*0.42f,h*0.03f,w*0.16f,h*0.055f),"RESTART")) { hp1=hp2=100; p1=0.25f; p2=0.75f; message="STICK CLASH 2D"; }
    }

    private void DrawStick(float x, float y, Color c, bool attacking)
    {
        float s = Mathf.Clamp(Screen.height*0.001f,0.45f,1.2f); GUI.color=c;
        GUI.DrawTexture(new Rect(x-14*s,y-66*s,28*s,28*s),Texture2D.whiteTexture);
        GUI.DrawTexture(new Rect(x-5*s,y-38*s,10*s,43*s),Texture2D.whiteTexture);
        GUI.DrawTexture(new Rect(x-30*s,y-30*s,25*s,7*s),Texture2D.whiteTexture);
        GUI.DrawTexture(new Rect(x+5*s,y-30*s,(attacking?45:25)*s,7*s),Texture2D.whiteTexture);
        GUI.DrawTexture(new Rect(x-20*s,y+3*s,7*s,38*s),Texture2D.whiteTexture);
        GUI.DrawTexture(new Rect(x+10*s,y+3*s,7*s,38*s),Texture2D.whiteTexture); GUI.color=Color.white;
    }
}
