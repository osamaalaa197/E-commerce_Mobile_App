import React, { useEffect, useState } from "react";
import {
  View,
  Text,
  StyleSheet,
  TextInput,
  FlatList,
  ActivityIndicator,
  SafeAreaView,
  TouchableOpacity,
} from "react-native";
import { useNavigation } from "@react-navigation/native";
import { Ionicons } from "@expo/vector-icons";
import ProductCardView from "./ProductCardView";
import { SIZES } from "../../constants";
import axios from "axios";


export default function ProductList(){
    const [data, setData] = useState([]);
    useEffect(() => {
      const fetchData = async () => {
        try {
          const response = await axios.get("https://localhost:7120/api/Product");
          setData(response.data);
        } catch (error) {
          console.error("Error fetching data:", error);
        }
      };
  
      fetchData();
    }, []);
    console.log("data",data)
    return(
        <View style={styles.container}>
            <FlatList data={data}
            numColumns={2}
            renderItem={({item})=><ProductCardView item={item}/>}
            contentContainerStyle={styles.container}
            ItemSeparatorComponent={()=>(<View style={styles.separator}/>)}
            
            />
        </View>
    )
}


const styles=StyleSheet.create({
    container:{
        alignItems:"center",
        paddingTop:SIZES.xxLarge,
        paddingLeft:SIZES.small/2
    },
    separator:{
        height:16,
    }
})