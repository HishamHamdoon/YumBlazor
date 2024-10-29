 namespace YumBlazor.Services
{
    public class SharedStateService
    {
        public event Action Onchange;
        private int _totalCartCount;
        public int TotalCartCount
        {
            get => _totalCartCount;

            set
            {
                _totalCartCount = value;
                NotifyStateChanged();
            }
        }
            private void NotifyStateChanged()=>Onchange?.Invoke();
    }
}
