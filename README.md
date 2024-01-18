# PickupableItemFactoryWithFlyweightPattern
Pickup edilince player'ın belirli özelliklerini iyileştiren item'ların spawn edildiği bir Factory ve Flyweight sistemi içeren basit bir FPS prototipini içerir.<br>

Bu repo, daha önce geliştirilmiş olan FPS prototipinin 3.Versiyonudur(v3). Bu Readme'de sadece v3'te değiştirilen veya eklenen kısımlar açıklanacaktır.<br> Diğer sınıfların açıklamaları için : <br> v2 => https://github.com/Unitarian2/BasicAdapterExample <br> v1 => https://github.com/Unitarian2/ObserverPattern-MVP-Based-User-Interface <br><br>

Prototipin 3. versiyonunda, daha sonradan player'ın pickup ederek belirli stat'larını artıracağı bir pickupable objesi yaratım sürecinin ilk hali oyuna implemente edilmiştir. Bu implementasyon sırasında Flyweight Pattern kullanılarak, genişletilebilir bir yapı hedeflenmiştir.<br><br>

---Flyweight Base Classes(Prototypes)---<br>
Flyweight.cs => Flyweight Pattern kullanarak data ve logic ayıracağımız GameObject'lerin miras alacağı sınıftır. Bu sınıf miras alındığında, bir Scriptable Object olan FlyweightSettings sınıfına erişim sağlanmış olur.
FlyweightSettings.cs => Flyweight miras alan sınıflar FlyweightSettings sınıfına erişim sağlar. Ancak bu ortak sınıfı kullanmak yerine kendi FlyweightSettings sınıflarını yaratıp, Create methodunu override edip kullanabilirler(bkz. PickupableItemSettings.cs). Bu sınıf içerisindeki Create methodu, yeni bir Gameobject instantite ederek, bu Gameobject'e Flyweight component'ini ekler ve kendisini settings olarak atama yapar. Bu şekilde sahnede flyweight yapısına bağlanmış bir Gameobject spawn olmuş olur. OnGet, OnRelease ve OnDestroyPoolObject methodları ise Unity'nin Object Pooling sistemini kullanan Factory'nin çalıştırarak sahneye spawn edilen Gameobject'i manipüle ettiği methodlardır.

---Pickupable Item Classes---<br>
PickupableItemSettings.cs => Bu sınıf FlyweightSettings'ten miras alarak onun bütün özelliklerine sahip olur. Ancak Create methodunu override ederek Flyweight component'i yerine PickupableItem component'ini ekler. FlyweightSettings sınıfındaki OnGet, OnRelease ve OnDestroyPoolObject methodları aynı şekilde bu sınıftada çalışır. 
