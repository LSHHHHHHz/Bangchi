using UnityEngine;

public class BattleManager : MonoBehaviour
{

    // 정적 필드
    // public "static" 타입명 필드명;
    public static BattleManager instance;

    public Player player;

    private void Awake()
    {
        // 변수 = 값
        // 변수 <== 값을 넣는다.
        // 변수 ==> 값 불가
        // this : 값이 자기 자신으로 정해져 있음. 값을 바꿀 수 없음.

        BattleManager.instance = this;
    }
}