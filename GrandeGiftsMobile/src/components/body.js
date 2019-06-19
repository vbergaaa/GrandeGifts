import React, { Component } from 'react';
import { StyleSheet, ScrollView, Text, View, Image } from 'react-native';

export default class Body extends Component {
    constructor(props) {
        super(props);
    }

    generateList() {
        const data = this.props.data;
        let JSX = [<View key="0"><Text style={styles.heading}>Hampers: {data.length}</Text></View>];
        let i = 0;
        data.forEach((cat) => {
            i++;
            if (cat.picture === null) {
                cat.picture = "DefaultHamper.png";
            }
            JSX.push(
                <View style={styles.card} key={"a" + i}>
                    <View style={styles.pictureContainer}>
                        <Image style={styles.picture} source={{ uri:"https://grandegiftsryanvandenberg.azurewebsites.net/uploads/hampers/" + cat.picture}}/>
                    </View> 
                    <View style={styles.cardContentContainer}>
                        <Text style={styles.cardTitle} key={"c" + i}>
                            {cat.hamperName}
                        </Text>
                        <Text style={styles.cardPrice} key={"d" + i}>
                            Price: ${cat.hamperPrice}
                        </Text>
                        <Text style={styles.cardDesc} key={"f" + i}>
                            {cat.hamperDescription}
                        </Text>
                    </View>
                </View>);
            }
        );
        
        return JSX;
    }

    render() {
        const JSX = this.generateList(); 
        return (
            <ScrollView style={styles.scrollView}>
                <View style={styles.container}>
                    {JSX}
                </View>
            </ScrollView>
        );
    }
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        width: '100%',
        padding:12,
        backgroundColor: '#fff',
        alignItems: 'center',
        justifyContent: 'flex-start',
    },
    card: {
        height: 115,
        borderRadius: 10,
        width: '100%',
        marginTop:15,
        backgroundColor: '#eee',
        borderColor: '#555',
        borderWidth: 2,
        padding: 2,
        flexDirection: 'row'
    },
    heading: {
        fontSize: 24
    },
    cardTitle: {
        fontSize: 20,
        marginLeft: 16,
        marginRight: 16
    },
    cardDesc: {
        fontSize: 16,
        marginLeft: 16,
        marginRight: 16
    },
    scrollView: {
        width:'100%'
    },
    pictureContainer: {
        width: 100,
        height:'100%',
        flexDirection: 'column',
        alignItems: 'center',
        justifyContent: 'center'
    },
    picture: {
        height: 70,
        width:70,
    },
    cardContentContainer: {
        flex:1
    }
});
