export class MenuModel{
    name: string = "";
    icon: string = "";
    url: string = "";
    isTitle: boolean = false;
    subMenus: MenuModel[] = [];
}

export const Menus: MenuModel[] = [
    {
        name: "Ana Sayfa",
        icon: "fa-solid fa-home",
        url: "/",
        isTitle: false,
        subMenus: []
    },   
    {
        name: "Admin",
        icon: "",
        url: "",
        isTitle: true,
        subMenus: []
    },
    {
        name: "Kullanıcılar",
        icon: "fa-solid fa-users",
        url: "/users",
        isTitle: false,
        subMenus:[]
    },
   
]