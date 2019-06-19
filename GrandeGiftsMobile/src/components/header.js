import React, { Component } from 'react';
import { StyleSheet, Text, TextInput, View, Button, Image } from 'react-native';

export default class Header extends Component {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <View style={styles.mainContainer}>
                <View style={styles.brandContainer}>
                    <Image style={styles.brandPicture} source={{ uri: "https://grandegiftsryanvandenberg.azurewebsites.net/images/logo.webp" }} />
                    <Text style={styles.brandTitle}>GrandeGifts</Text>
                </View>
                <View style={styles.SearchContainer}>
                    <View style={styles.inputContainer}>
                        <Text style={styles.textTitle}>Search By Category:</Text>
                        <TextInput style={styles.textInput} onChangeText={this.props.onSearchChange}>{this.props.search}</TextInput>
                    </View>
                    <View style={styles.buttonContainer}>
                        <Button style={styles.button} title="Search" onPress={this.props.searchOnPress} />
                    </View>
                </View>
                
            </View>
        );
    }
}

const styles = StyleSheet.create({
    mainContainer: {
        marginTop: 24,
        height: 120,
        width: '100%',
        backgroundColor: '#333',
        alignItems: 'center',
        justifyContent: 'space-between',
        flexDirection: 'column'
    },
    brandContainer: {
        height: 60,
        flexDirection: 'row',
        alignItems: 'center',
        justifyContent: 'space-around'
    },
    brandPicture: {
        height: 60,
        width:60
    },

    brandTitle: {
        color: 'white',
        fontSize: 30,
        marginLeft:24
    },
    SearchContainer: {
        height: 60,
        alignItems: 'center',
        justifyContent: 'space-between',
        flexDirection: 'row'
    },
    inputContainer: {
        flex: 1,
        padding: 6,
        alignItems: 'center',
        flexDirection:'row'
    },
    buttonContainer: {
        width: 90,
    },
    textTitle: {
        color: '#fff',
        marginRight:12
    },
    textInput: {
        borderRadius:5,
        color: '#111',
        width: 100,
        backgroundColor: '#fff',
        padding: 6,
        flex:1
    },
    button: {
        marginLeft:'auto'
    }
});
