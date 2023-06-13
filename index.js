Vue.createApp({
    data() {
        return {
            items: [{"Id": 1, "RoomNo": "D3.17", "Temp_C": 19, "Day": "Monday"}, 
            {"Id": 2, "RoomNo": "D3.17", "Temp_C": 25, "Day": "Friday"}, 
            {"Id": 3, "RoomNo": "D3.14", "Temp_C": 17, "Day": "Monday"}, 
            {"Id": 4, "RoomNo": "D3.14", "Temp_C": 19, "Day": "Friday"}, 
            {"Id": 5, "RoomNo": "D2.11", "Temp_C": 22, "Day": "Monday"},
            {"Id": 6, "RoomNo": "D2.11", "Temp_C": 21, "Day": "Friday"}],
            RoomNo: "",
            Temp_C: 0,
            Day: "",
            SearchRoom: "",
            GottenRoom: [],
        }
    },
    
  methods: {
    addNew() {
        //Tilføjer baseret på bruger indtastede værdier en ny item og øger automatisk id'et med 1.
        //Derefter resettes værdierne så f.eks day ikke bliver til "MondayFridayThursday" etc.
        const newId = this.items.length + 1
        Data = {"Id": newId, "RoomNo": this.RoomNo, "Temp_C": this.Temp_C, "Day": this.Day}
        this.items.push(Data)
        this.RoomNo = ""
        this.Temp_C = 0
        this.Day = ""
    },
    
    getRoom() {
        //Tilsvarende foreach i vue/JS finder item hvor roomno er identisk med input searchroom og 
        //tilføjer det til gottenroom
        for (let item of this.items) {
            if (item.RoomNo === this.SearchRoom) {
                this.GottenRoom.push(item);
            }
        }
    },
    deleteAll() {
        this.items = []
    }
    
  },
}).mount("#app")
