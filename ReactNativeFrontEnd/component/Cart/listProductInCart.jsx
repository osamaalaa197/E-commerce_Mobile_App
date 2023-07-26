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
import ProductCardView from "../Products/ProductCardView";
import { SIZES } from "../../constants";
import axios from "axios";
import AsyncStorage from "@react-native-async-storage/async-storage";

export default function ListProductInCart() {
  const [data, setData] = useState([]);
  const [token, setToken] = useState("");
  const [userID, setUserID] = useState("");
  // useEffect(() => {
  //   const fetchData = async () => {
  //     try {
  //       const response = await axios.get(
  //         `https://localhost:7120/api/Cart?userid=${userID}`,
  //         {
  //           headers: {
  //             Authorization: `Bearer ${token}`,
  //             "Content-Type": "application/json",
  //           },
  //         }
  //       );
  //       console.log("response", response.data[0].cartProdcts);
  //       setData(response.data[0].cartProdcts);
  //     } catch (error) {
  //       console.error("Error fetching data:", error);
  //     }
  //   };
  //   const getData = async () => {
  //     const token = await AsyncStorage.getItem("token");
  //     const userID = await AsyncStorage.getItem("userID");
  //     setToken(token);
  //     setuserID(userID);
  //     fetchData();
  //   };
  //   getData()
  // }, []);

  // const userID=localStorage.getItem("userID")
  // const token=localStorage.getItem("token")

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await axios.get(
          `https://localhost:7120/api/Cart?userid=${userID}`,
          {
            headers: {
              Authorization: `Bearer ${token}`,
              "Content-Type": "application/json",
            },
          }
        );
        console.log("response", response.data[0].cartProdcts);
        setData(response.data[0].cartProdcts);
      } catch (error) {
        console.error("Error fetching data:", error);
      }
    };

    const getData = async () => {
      const token = await AsyncStorage.getItem("token");
      const userID = await AsyncStorage.getItem("userID");
      setToken(token);
      setUserID(userID);
      fetchData(); // Call the fetchData function once you have the token and userID
    };
    // fetchData(); // Call the fetchData function once you have the token and userID

    getData();
  }, []);
  console.log("data", data);
  return (
    <View style={styles.container}>
      <FlatList
        data={data}
        numColumns={2}
        renderItem={({ item }) => <ProductCardView item={item} />}
        contentContainerStyle={styles.container}
        ItemSeparatorComponent={() => <View style={styles.separator} />}
      />
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    alignItems: "center",
    paddingTop: SIZES.xxLarge,
    paddingLeft: SIZES.small / 2,
  },
  separator: {
    height: 16,
  },
});
