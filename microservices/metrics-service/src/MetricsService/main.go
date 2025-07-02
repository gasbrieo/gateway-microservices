package main

import (
	"fmt"
	"log"
	"net/http"
)

func pingHandler(w http.ResponseWriter, r *http.Request) {
	fmt.Fprint(w, "pong from MetricsService")
}

func main() {
	http.HandleFunc("/api/v1/ping", pingHandler)

	port := "7000"
	fmt.Println("MetricsService listening on port", port)
	log.Fatal(http.ListenAndServe(":"+port, nil))
}
