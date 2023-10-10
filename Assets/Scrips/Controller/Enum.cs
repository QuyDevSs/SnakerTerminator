public enum GameStates
{
    Menu,
    LevelMap,
    Upgrade,
    Playing,
    Pause,
    End
}
public enum EnemyTypes
{
    Normal,
    Archer,
    Speed,
    Tanker,
    Boss
}
public enum BodyTypes
{
    Normal,
    Light,
    Fire,
    Ice,
    Plants,
    Dark,
    Tail,
    GunTurret
}
public enum BulletTypes
{
    Normal, // Di chuyển theo mục tiêu, Trúng tường destroy, trúng quái destroy
    Light, //tạo tia sét tấn công mục tiêu đến chết hoặc ra khỏi tầm
    Fire, //Di chuyển theo mục tiêu, Nảy sang mục tiêu phía trước theo khoảng cách, giới hạn sỗ lần nảy
    Ice,
    Plants, //Di chuyển về phía trước, xuyên qua mục tiêu, trúng vật cản hoặc màn hình sẽ nảy lại
    Dark, //tạo 3 kiếm ánh sáng xoay quanh bản thân
    Circle,
    Enemy
}
// nổ , làm chậm, đẩy lùi, thiêu đốt, choáng, triệu hồi, hủy đạn đạo