using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class SetupCamera
    {

        private bool _isVerticale;
        private float _zPos = -10f;

        public void SetCamera(int width, int height, bool isVerticale)
        {
            _isVerticale = isVerticale;

            var xPos = width / 2f - 0.5f;
            var yPos = height / 2f + 0.5f;

            Camera.main.gameObject.transform.position = new Vector3(xPos, yPos, _zPos);
            Camera.main.orthographicSize = GetOrthoSize(width, height);
        }

        private float GetOrthoSize(int width, int height)
        {
            return _isVerticale ? (width + 1f) * Screen.height / Screen.width * 0.5f :
                (height + 3f) * Screen.height / Screen.width;
        }
    }
}
