# PickupableItemFactoryWithFlyweightPattern
Pickup edilince player'ın belirli özelliklerini iyileştiren item'ların spawn edildiği bir Factory ve Flyweight sistemi içeren basit bir FPS prototipini içerir.<br>

Bu repo, daha önce geliştirilmiş olan FPS prototipinin 3.Versiyonudur(v3). Bu Readme'de sadece v3'te değiştirilen veya eklenen kısımlar açıklanacaktır.<br> Diğer sınıfların açıklamaları için : <br> v2 => https://github.com/Unitarian2/BasicAdapterExample <br> v1 => https://github.com/Unitarian2/ObserverPattern-MVP-Based-User-Interface <br><br>

Prototipin 3. versiyonunda, daha sonradan player'ın pickup ederek belirli stat'larını artıracağı bir pickupable objesi yaratım sürecinin ilk hali oyuna implemente edilmiştir. Bu implementasyon sırasında Flyweight Pattern kullanılarak, genişletilebilir bir yapı hedeflenmiştir.<br><br>

---Flyweight Base Classes(Prototypes)---<br>
<b>Flyweight.cs =></b> Flyweight Pattern kullanarak data ve logic ayıracağımız GameObject'lerin miras alacağı sınıftır. Bu sınıf miras alındığında, bir Scriptable Object olan FlyweightSettings sınıfına erişim sağlanmış olur.<br>
<b>FlyweightSettings.cs =></b> Flyweight miras alan sınıflar FlyweightSettings sınıfına erişim sağlar. Ancak bu ortak sınıfı kullanmak yerine kendi FlyweightSettings sınıflarını yaratıp, Create methodunu override edip kullanabilirler(bkz. PickupableItemSettings.cs). Bu sınıf içerisindeki Create methodu, yeni bir Gameobject instantiate ederek, bu Gameobject'e Flyweight component'ini ekler ve kendisini settings olarak atama yapar. Bu şekilde sahnede flyweight yapısına bağlanmış bir Gameobject spawn olmuş olur. OnGet, OnRelease ve OnDestroyPoolObject methodları ise Unity'nin Object Pooling sistemini kullanan Factory'nin çalıştırarak sahneye spawn edilen Gameobject'i manipüle ettiği methodlardır.<br><br>

---Pickupable Item Classes---<br>
<b>PickupableItemSettings.cs =></b> Bu sınıf FlyweightSettings'ten miras alarak onun bütün özelliklerine sahip olur. Ancak Create methodunu override ederek Flyweight component'i yerine PickupableItem component'ini ekler. FlyweightSettings sınıfındaki OnGet, OnRelease ve OnDestroyPoolObject methodları aynı şekilde bu sınıftada çalışır. <br>
<b>PickupableItem.cs =></b> Bu sınıf, spawn edilen bir pickupable Gameobject'ine eklenmiş olan bir component'dir. Ekleme sırasında PickupableItemSettings'i de referans olarak aldığı için, PickupableItemSettings içerisinde belirtilen verilere ve methodlara sahiptir. Bu artık istenen şekilde manipüle edilebilecek hazır bir Gameobject halindedir. Örneğin Gamedesigner herhangi bir özelliğini editlemek istediğinde, PickupableItemSettings'ten türetilmiş Scriptable Object'i editlemesi yeterlidir(bkz.PickupableItemHealth.asset, PickupableItemMana.asset).<br>
<b>PickupableFactory.cs =></b> Unity'de bulunan IObjectPool interface'ini kullanarak bir Object Pooling sistemi yürüten bir FactoryClass'ıdır. Pickupable sınıflarına özel olarak oluşturulmuştur. Static methodları ile istenen anda bu sınıfa erişilip Spawn ve ReturnToPool methodları kullanılabilir. Bu sınıfta Unity Object Pooling sisteminin methodları olan Get() ve Release(f) methodları çağırılmadan önce ilgili nesnenin hangi Pool'a ait olduğunu bulmak için GetRelatedPool methodu çağırılır. GetRelatedPool methodu önce aranan nesnenin tipinde bir Pool var mı kontrol ederek eğer yoksa yeni bir Pool yaratır(new ObjectPool<Flyweight>). Böylece Flyweight miras alarak çalışan tüm Object'ler kolayca Pooling sisteme dahil edilebilir.<br>
<b>PickupableSpawner.cs =></b> Spawn işlemlerinin yapıldığı Manager class'ıdır. PickupableItemSettings tipinde Scriptable Object'lerinin bir listesini tutar ve içlerinden rastgele birtanesini seçer. Ardından PickupableFactory'nin static methodu olan Spawn methodu ile Pool'dan bir Flyweight Gameobject alarak gerekli işlemleri yapar. Bu işlem sonunda Gameobject sahnededir.<br><br>
