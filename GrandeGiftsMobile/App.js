import React from 'react';
import { StyleSheet, Text, View } from 'react-native';
import Body from './src/components/body';
import Header from './src/components/header';
import ApiService from './src/services/apiService';

export default class App extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            search: "",
            hampers: [],
        };
    }

    componentDidMount() {
        const apiService = new ApiService();
        apiService.GetAllHampers((data) => { this.setState({ hampers: data }); });
    }

    onSearchChange(val) {
        this.setState({ search: val });
    }

    onSearch() {
        const apiService = new ApiService();
        apiService.GetHampersByCategory(this.state.search,(data) => { this.setState({ hampers: data }); });
    }

  render() {
    return (
      <View style={styles.container}>
            <Header search={this.state.search} onSearchChange={this.onSearchChange.bind(this)} searchOnPress={this.onSearch.bind(this)}/>
            <Body data={this.state.hampers}/>
      </View>
    );
  }
}

const styles = StyleSheet.create({
  container: {
        flex: 1,
    backgroundColor: '#999',
    alignItems: 'center',
    justifyContent: 'center',
  },
});
