package com.example.memseri;

import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.stage.Stage;

public class MainApp extends Application {

    @Override
    public void start(Stage stage) throws  Exception {


        FXMLLoader fxmlLoader = new FXMLLoader(getClass().getResource("/com/example/memseri/fxml/main-view.fxml"));
        Parent root = fxmlLoader.load();
        Scene scene = new Scene(root, 400, 300);

        stage.setTitle("JavaFX Demo");
        stage.setScene(scene);
        stage.show();
    }

    public static void main(String[] args) {
        launch();
    }
}
