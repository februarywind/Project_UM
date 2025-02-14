public enum EAtackElement
{
    Normal, Electric, Grass, Size
}

public interface IDamagable
{
    void TakeDamage(float damage, EAtackElement element);
}
