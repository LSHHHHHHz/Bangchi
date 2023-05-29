using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.CSharp
{
    internal class VariableExample
    {
        // 변수(멤버 필드 형식) 선언
        // 변수 : 값을 저장하는 저장소
        // 상수 : 변하지 않는 값을 나타냄.
        // 변수 : 변하는 값을 가리키는 것, 상수 : 변하지 않는 값을 가리키는 것.
        // 값 : 어떠한 객체 자체나 1, 2, 3과 같은 숫자, "string", false, true


        // 값 타입 : 각각 타입마다 고유한 기본값이 있음.
        private int intField; // 기본값 : 0
        private bool boolField; // 기본값 : false

        // 참조 타입 : 기본값이 null
        private GameObject gameObjectField;
        private string stringField;

        // 상수 선언 : 변수 타입앞에 'const'를 붙여주고, 그 즉시 값을 초기화( = 3;) 시켜주면 됨.
        // 값을 그 즉시 초기화 시켜주지 않으면 값이 바뀔 가능성이 있다는 뜻이기 때문에 무조건 초기화 시켜줘야 함.
        private const int constIntField = 3;

        private void UseVariables()
        {
            // null을 넣을 수 있는 타입과 안되는 타입이 있음.
            // int, bool, struct(구조체), enum 과 같은 타입들은 "값타입(Value Type)"임. 값타입은 null을 넣을 수 없음.

            // GameObject, Object, string, GachaPopup, Component같은 타입들은 "참조타입(Reference Type)"임. 참조타입은 null을 넣을 수 있음.
            if (gameObjectField == null) // gameObjectField가 null이라면
            {
                gameObjectField = new GameObject("My GameObject");
            }

            if (gameObjectField != null) // gameObjectField가 null이 아니라면
            {
                Debug.Log(gameObjectField.name); // My GameObject
            }

            GameObject.Destroy(gameObjectField);
            gameObjectField = null;
        }
    }
}
